package com.example.PaymentService.data;

import com.example.PaymentService.model.Payment;

public class RepoPayment implements IRepoPayment {
    @Override
    public Payment simulatePaymentProcessing(Payment payment) {
        try {
            Thread.sleep(2000); // Simulate processing delay
            if (Math.random() > 0.2) { // 80% success rate
                payment.setStatus("SUCCESS");
                payment.setSimulationDetails("Payment processed successfully in simulation.");
            } else {
                payment.setStatus("FAILED");
                payment.setSimulationDetails("Payment failed in simulation.");
            }
        } catch (InterruptedException e) {
            payment.setStatus("FAILED");
            payment.setSimulationDetails("Payment simulation interrupted.");
        }

        return payment;
    }
}
