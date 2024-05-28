package com.example.PaymentService.data;

import com.example.PaymentService.model.Payment;

public interface IRepoPayment {
    Payment simulatePaymentProcessing(Payment payment);
}
