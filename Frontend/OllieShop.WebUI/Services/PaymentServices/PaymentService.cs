﻿using OllieShop.DtoLayer.PaymentDtos;

namespace OllieShop.WebUI.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("payments", createPaymentDto);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync("payments", updatePaymentDto);
            return responseMessage;
        }

        public async Task<GetByIdPaymentDto> GetByIdPaymentAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"payments/{id}");
            responseMessage.EnsureSuccessStatusCode(); // Ensure status code 200-299
            var payment = await responseMessage.Content.ReadFromJsonAsync<GetByIdPaymentDto>();
            return payment;
        }

        public async Task<GetByIdPaymentDto> GetByOrderIdPaymentAsync(string orderId)
        {
            var responseMessage = await _httpClient.GetAsync($"payments/order/{orderId}");
            responseMessage.EnsureSuccessStatusCode(); // Ensure status code 200-299
            var payment = await responseMessage.Content.ReadFromJsonAsync<GetByIdPaymentDto>();
            return payment;
        }
    }
}
