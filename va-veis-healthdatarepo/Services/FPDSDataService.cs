using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Errors;
using va_veis_healthdatarepo.Models.Interfaces;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Services
{
    public class FPDSDataService<T> : BaseService<FPDSDataService<T>>, IFPDSDataService<T> where T : new()
    {
        public IErrorEvaluator ErrorEvaluator { get; set; }

        public Dictionary<string, string> RequestParameters { get; set; }

        public FPDSDataService(IDependencyAggregate<FPDSDataService<T>> aggregate) : base(aggregate)
        {
            ErrorEvaluator = new ErrorEvaluator();
            RequestParameters = new Dictionary<string, string>();
        }

        //static void SetNumberHandlingModifier(JsonTypeInfo jsonTypeInfo)
        //{
        //    if (jsonTypeInfo.Type == typeof(uint))
        //    {
        //        jsonTypeInfo.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        //    }
        //}

        public IFPDSRequestMessageFactory<T> FPDSRequestMessageFactory { get; set; }
        public async Task<FPDSResponseMessage<T>> GetDataAsync(string clientName, string nationalID, string requestId = null, string startDate = null, string endDate = null)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
            if (string.IsNullOrEmpty(nationalID)) throw new ArgumentNullException("nationalID");

            var response = new FPDSResponseMessage<T>();
            var dataList = new List<T>();
            var warningSiteIds = new List<string>();

            var utils = new RequestUtilities(_logger);
            var dataRequest = new FPDSRequestMessage<T>(requestId, _fpdsServiceConfigs.Value);

            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("clientName", clientName));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("nationalId", nationalID));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("_type", "json"));

            if (!string.IsNullOrEmpty(startDate))
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>("startDate", startDate));
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>("endDate", endDate));
            }

            // add domain specific request parameters
            foreach (var parameter in RequestParameters)
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>(parameter.Key, parameter.Value));
            }

            try
            {
                var client = _httpClientFactory.CreateClient("hdr");
                client.BaseAddress = new Uri(dataRequest.EndPointUrl);
                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };
                var rawStingResponse = await utils.ExecuteAsync(client, dataRequest);

                JsonSerializerOptions options = new()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                };

                var dataResponse = JsonSerializer.Deserialize<FPDSRoot<T>>(rawStingResponse, options);

                if (dataResponse.Sites == null) throw new ApplicationException("Null site returned in FPDS response");

                foreach (var site in dataResponse.Sites)
                {
                    if (ErrorEvaluator.hasError(site.ErrorSection))
                    {
                        response.ErrorOccurred = true;
                    }

                    if (site.Data != null)
                    {
                        if (site.Data.TotalItems > 0)
                        {
                            dataList.AddRange(site.Data.Items);
                        }
                    }

                    else
                    {
                        if (site.Error != null && site.Error.Message != null)
                        {
                            response.WarningMessage = "An error occurred getting data from one or more sites. Please navigate to VistA/CPRS for any additional information.";
                        }

                        if (site.ErrorSection != null)
                        {
                            response.ErrorOccurred = true;

                            var hdrError = ParseHDRErrors(site.ErrorSection);

                            hdrError.ErrorCode = hdrError.ErrorCode ?? "";
                            response.ErrorMessage = hdrError.ExceptionMessage;
                            response.DebugInfo = hdrError.ToStringBuilder().ToString();

                            if (hdrError.ErrorCode.ToUpper() == HDRErrorTypes.ReadRequestDataSourceFailure)
                            {
                                const string searchString = "Assigning Facility is";

                                var startIndex = hdrError.DisplayMessage.IndexOf(searchString, StringComparison.Ordinal) + searchString.Length;
                                var nextPeriod = hdrError.DisplayMessage.IndexOf(".", startIndex, StringComparison.Ordinal);
                                var siteId = hdrError.DisplayMessage.Substring(startIndex, nextPeriod - startIndex).Trim();

                                warningSiteIds.Add(siteId);
                            }
                        }
                    }
                } // end site loop

                if (warningSiteIds.Count > 0)
                {
                    var joinedIds = string.Join(", ", warningSiteIds);
                    response.WarningMessage = string.Format("Unable to retrieve data from the following facilities: {0}. Please navigate to VistA/CPRS for this information.", joinedIds);
                }

                if (response.ErrorOccurred)
                {
                    return response;
                }

                response.Data = dataList;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;

                _logger.LogError(
                    string.Format(
                        "FPDSData::GetData(): Error making request to {0}/{1} for Client: {2} NationalID: {3} Details: {4}",
                        dataRequest.BaseUrl, dataRequest.Resource, clientName, nationalID, errorMsg), ex);

                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = errorMsg;
                response.DebugInfo = errorMsg;
            }

            return response;
        }

        public async Task<FPDSResponseMessage<T>> GetDataByTypeAsync<T>(string clientName, string nationalID, string requestId = null, string startDate = null, string endDate = null)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
            if (string.IsNullOrEmpty(nationalID)) throw new ArgumentNullException("nationalID");

            var response = new FPDSResponseMessage<T>();
            var dataList = new List<T>();
            var warningSiteIds = new List<string>();

            var utils = new RequestUtilities(_logger);
            var dataRequest = new FPDSRequestMessage<T>(requestId, _fpdsServiceConfigs.Value);

            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("clientName", clientName));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("nationalId", nationalID));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("_type", "json"));

            if (!string.IsNullOrEmpty(startDate))
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>("startDate", startDate));
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>("endDate", endDate));
            }

            // add domain specific request parameters
            foreach (var parameter in RequestParameters)
            {
                dataRequest.URLParameters.Add(new KeyValuePair<string, string>(parameter.Key, parameter.Value));
            }

            try
            {
                var client = _httpClientFactory.CreateClient("hdr");
                client.BaseAddress = new Uri(dataRequest.EndPointUrl);
                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };
                var dataResponse = await utils.ExecuteAsync<FPDSRoot<T>>(client, dataRequest);

                if (dataResponse.Sites == null) throw new ApplicationException("Null site returned in FPDS response");

                if (dataResponse.Sites.Any(s => s.ErrorSection != null && s.ErrorSection.FatalErrors.Any()))
                {
                    var errorSection = dataResponse.Sites.FirstOrDefault(s => s.ErrorSection != null).ErrorSection;
                    response.ErrorOccurred = true;
                    response.ErrorMessage = errorSection?.FatalErrors.FirstOrDefault().ExceptionMessage;
                    response.DebugInfo = errorSection?.FatalErrors.FirstOrDefault().ToString();
                }

                foreach (var site in dataResponse.Sites)
                {
                    if (ErrorEvaluator.hasError(site.ErrorSection))
                    {
                        response.ErrorOccurred = true;
                    }

                    if (site.Data != null)
                    {
                        if (site.Data.TotalItems > 0)
                        {
                            dataList.AddRange(site.Data.Items);
                        }
                    }

                    else
                    {
                        if (site.Error != null && site.Error.Message != null)
                        {
                            response.WarningMessage = "An error occurred getting data from one or more sites. Please navigate to VistA/CPRS for any additional information.";
                        }

                        if (site.ErrorSection != null)
                        {
                            var hdrError = ParseHDRErrors(site.ErrorSection);

                            hdrError.ErrorCode = hdrError.ErrorCode ?? "";
                            response.ErrorMessage = hdrError.ExceptionMessage;
                            response.DebugInfo = hdrError.ToStringBuilder().ToString();

                            if (hdrError.ErrorCode.ToUpper() == HDRErrorTypes.ReadRequestDataSourceFailure)
                            {
                                const string searchString = "Assigning Facility is";

                                var startIndex = hdrError.DisplayMessage.IndexOf(searchString, StringComparison.Ordinal) + searchString.Length;
                                var nextPeriod = hdrError.DisplayMessage.IndexOf(".", startIndex, StringComparison.Ordinal);
                                var siteId = hdrError.DisplayMessage.Substring(startIndex, nextPeriod - startIndex).Trim();

                                warningSiteIds.Add(siteId);
                            }
                        }
                    }
                } // end site loop

                if (warningSiteIds.Count > 0)
                {
                    var joinedIds = string.Join(", ", warningSiteIds);
                    response.WarningMessage = string.Format("Unable to retrieve data from the following facilities: {0}. Please navigate to VistA/CPRS for this information.", joinedIds);
                }

                if (response.ErrorOccurred)
                {
                    return response;
                }

                response.Data = dataList;
            }
            catch (JsonException ex)
            {
                if (dataList?.Count == 0 && ex.Message.Contains("is invalid within a JSON string. The string should be correctly escaped"))
                    _logger.LogError(
                    string.Format(
                        "FPDSData::GetData(): Error making request to {0}/{1} for Client: {2} NationalID: {3} Details: {4}",
                        dataRequest.BaseUrl, dataRequest.Resource, clientName, nationalID, ex.Message), ex);

                else
                    throw;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;

                _logger.LogError(
                    string.Format(
                        "FPDSData::GetData(): Error making request to {0}/{1} for Client: {2} NationalID: {3} Details: {4}",
                        dataRequest.BaseUrl, dataRequest.Resource, clientName, nationalID, errorMsg), ex);

                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = errorMsg;
                response.DebugInfo = errorMsg;
            }

            return response;
        }

        public async Task<FPDSResponseMessage<Flag>> GetFlagDataAsync(string clientName, string nationalID, string requestID)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
            if (string.IsNullOrEmpty(nationalID)) throw new ArgumentNullException("nationalID");

            var response = new FPDSResponseMessage<Flag>();
            var dataList = new List<Flag>();
            var dataRequest = new FPDSRequestMessage<T>(requestID, _fpdsServiceConfigs.Value);

            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("clientName", clientName));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("nationalId", nationalID));
            dataRequest.URLParameters.Add(new KeyValuePair<string, string>("_type", "json"));

            try
            {
                var utils = new RequestUtilities(_logger);
                var client = _httpClientFactory.CreateClient("hdr");
                client.BaseAddress = new Uri(dataRequest.EndPointUrl);
                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };

                var dataResponse = await utils.ExecuteAsync<FlagsRoot>(client, dataRequest);

                if (dataResponse.Sites == null) throw new ApplicationException("Null site returned in FPDS response");

                if (dataResponse.Sites.Any(s => s.ErrorSection != null && s.ErrorSection.FatalErrors.Any()))
                {
                    var fatalError = dataResponse.Sites.FirstOrDefault(s => s.ErrorSection != null).ErrorSection.FatalErrors.FirstOrDefault();
                    response.ErrorOccurred = true;
                    response.ErrorMessage = fatalError.ExceptionMessage;
                    response.DebugInfo = fatalError.ToString();
                }

                foreach (var site in dataResponse.Sites)
                {
                    if (site.Results != null)
                    {
                        if (site.Results.Flags != null)
                        {
                            if (site.Results.Flags.Flag != null && site.Results.Flags.Flag is List<FPDSFlag>)
                            {
                                dataList.AddRange(TransformFPDSFlags((List<FPDSFlag>)site.Results.Flags.Flag));
                            }
                        }
                    }
                }

                response.Data = dataList;
            }

            catch (Exception ex)
            {
                var innerExp = ex.InnerException != null ? ex.InnerException.Message : "";
                var errorMsg = ex.Message + ": " + innerExp;

                _logger.LogError(
                    string.Format(
                        "FPDSData::GetFlagData(): Error making FPDSRequestMessage request to {0}/{1} for Client: {2} NationalID: {3} Details: {4}",
                        dataRequest.BaseUrl, dataRequest.Resource, clientName, nationalID, ex.Message), ex);

                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = "Error::: " + errorMsg;
                response.DebugInfo = errorMsg;
            }

            return response;
        }

        public async Task<FPDSResponseMessage<LabOrders>> GetLabDataAsync(string clientName, string nationalID, string requestId, string startDate, string endDate)
        {
            var response = new FPDSResponseMessage<LabOrders>();
            try
            {
                var labs = GetDataByTypeAsync<Lab>(clientName, nationalID, requestId, startDate, endDate);
                var orders = GetDataByTypeAsync<Order>(clientName, nationalID, requestId, startDate, endDate);

                var labOrderResults = Task.WhenAll(labs, orders);

                await labOrderResults.WaitAsync(new CancellationToken());

                response.Data = new List<LabOrders> {
                    new () {
                        Labs = labs.Result.Data,
                        Orders = orders.Result.Data
                    }
                };

                if (labs.Result.ErrorOccurred || orders.Result.ErrorOccurred)
                {
                    response.DebugInfo = labs.Result.ErrorOccurred
                        ? labs.Result.DebugInfo
                        : orders.Result.DebugInfo;
                    response.ErrorMessage = labs.Result.ErrorOccurred
                        ? labs.Result.ErrorMessage
                        : orders.Result.ErrorMessage;
                    response.ErrorOccurred = true;
                    response.Status = labs.Result.ErrorOccurred
                        ? labs.Result.Status
                        : orders.Result.Status;
                }

                return response;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                _logger.LogError($"Error: {errorMsg}");

                response.DebugInfo = errorMsg;
                response.ErrorMessage = errorMsg;
                response.ErrorOccurred = true;
                response.Status = "Error";

                return response;
            }
        }

        private List<Flag> TransformFPDSFlags(List<FPDSFlag> flagList)
        {
            var patientFlagsList = new List<Flag>();
            foreach (var flag in flagList)
            {
                var patientFlag = new Flag();
                if (flag.Id != null)
                {
                    patientFlag.Id = string.IsNullOrEmpty(flag.Id.Value) ? string.Empty : flag.Id.Value;
                }
                if (flag.Content != null)
                {
                    patientFlag.Content = string.IsNullOrEmpty(flag.Content.Value)
                        ? string.Empty
                        : flag.Content.Value;
                }
                if (flag.OwnSite != null)
                {
                    patientFlag.OwnSiteName = string.IsNullOrEmpty(flag.OwnSite.Name)
                        ? string.Empty
                        : flag.OwnSite.Name;
                    patientFlag.OwnSiteCode = flag.OwnSite.Code.ToString();
                }
                if (flag.Category != null)
                {
                    patientFlag.Category = string.IsNullOrEmpty(flag.Category.Value)
                        ? string.Empty
                        : flag.Category.Value;
                }
                if (flag.Name != null)
                {
                    patientFlag.Name = string.IsNullOrEmpty(flag.Name.Value) ? string.Empty : flag.Name.Value;
                }
                if (flag.OrigSite != null)
                {
                    patientFlag.OrigSiteName = string.IsNullOrEmpty(flag.OrigSite.Name)
                        ? string.Empty
                        : flag.OrigSite.Name;
                    patientFlag.OrigSiteCode = flag.OrigSite.Code.ToString();
                }
                if (flag.ApprovedBy != null)
                {
                    patientFlag.ApprovedByName = string.IsNullOrEmpty(flag.ApprovedBy.Name)
                        ? string.Empty
                        : flag.ApprovedBy.Name;
                    patientFlag.ApprovedByCode = flag.ApprovedBy.Code.ToString();
                }
                if (flag.Type != null)
                {
                    patientFlag.Type = string.IsNullOrEmpty(flag.Type.Value) ? string.Empty : flag.Type.Value;
                }
                if (flag.ReviewDue != null)
                {
                    patientFlag.ReviewDue = string.IsNullOrEmpty(flag.ReviewDue.Value.ToString())
                        ? string.Empty
                        : flag.ReviewDue.Value.ToString();
                }
                if (flag.Assigned != null)
                {
                    patientFlag.Assigned = flag.Assigned.Value.ToString();
                }
                patientFlagsList.Add(patientFlag);
            }
            return patientFlagsList;
        }

        private HDRErrors ParseHDRErrors(HDRErrorSection errorSections)
        {
            var hdrError = new HDRErrors();
            if (errorSections.Errors != null)
            {
                hdrError = errorSections.Errors.FirstOrDefault();
            }
            else if (errorSections.FatalErrors != null)
            {
                hdrError = errorSections.FatalErrors.FirstOrDefault();
            }
            else if (errorSections.Warnings != null)
                hdrError = errorSections.Warnings.FirstOrDefault();
            return hdrError;
        }
    }
}