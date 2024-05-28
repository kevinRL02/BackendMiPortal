package com.example.PaymentService.resources;

import jakarta.persistence.Id;

import java.time.LocalDateTime;

public class Payment {
    @Id
    private LocalDateTime createdAt;
    private String simulationDetails;
    private String status;

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        this.createdAt = createdAt;
    }

    public String getSimulationDetails() {
        return simulationDetails;
    }

    public void setSimulationDetails(String simulationDetails) {
        this.simulationDetails = simulationDetails;
    }


}
