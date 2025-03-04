﻿using SMS.Application.Common.Service;

namespace SMS.Application.Common.API
{
    public interface IApiClientService : ITransientService
    {
        Task<T?> GetAsync<T>(string endpoint, IDictionary<string, string>? parameters = null, IDictionary<string, string>? headersValue = null);

        Task<T?> PostAsync<T>(string endpoint, StringContent stringContent);

        Task<T?> UpdateAsync<T>(string endpoint, StringContent stringContent);

        Task<T?> DeleteAsync<T>(string endpoint);
    }
}
