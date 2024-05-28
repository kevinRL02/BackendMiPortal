package com.example.PaymentService.controllers;

import com.example.PaymentService.data.IRepoPayment;

import com.example.PaymentService.resources.Payment;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

import jakarta.ws.rs.GET;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.Response;
import java.time.LocalDateTime;
@Path("payment")
public class PaymentController {
    IRepoPayment repoPayment;
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    @Path("{simulate}")
    public com.example.PaymentService.resources.Payment hello(){
        com.example.PaymentService.resources.Payment payment = new Payment();
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
