import { check } from "k6";
import { Counter } from "k6/metrics";
import http from "k6/http";

const BASE_URL = __ENV.API_URL || "http://127.0.0.1:44359/";
const stringVUS = __ENV.VUS || "10";
const stringDURATIONS = __ENV.DURATION || "30s";
const endpointsString = __ENV.ENDPOINTS || "/defaultPath1,/defaultPath2,/defaultPath3";
const delimiter = __ENV.DELIMITER || ",";
const VUS = stringVUS.split(delimiter);
const DURATIONS = stringDURATIONS.split(delimiter);
const ENDPOINTS = endpointsString.split(delimiter);
const STAGES = [];
for (let i = 0; i < VUS.length; i++) {
  const duration = DURATIONS[i];
  const target = parseInt(VUS[i]);
  STAGES.push({ duration, target });
}
const stages = STAGES;

const requestTotal = new Counter("number_of_requests");
const requestSucceed = new Counter("number_of_requests_succeded");
const requestFail = new Counter("number_of_requests_failed");

// Definir la cantidad de solicitudes falsas
const numFailedRequests = 5; // por ejemplo, inyectaremos 5 solicitudes falsas

export const options = {
  thresholds: {
    http_req_failed: ["rate<0.01"],
    http_req_duration: ["p(95)<1000"],
  },
  stages: stages
};

export default function () {
  // Inyectar solicitudes falsas
  const requests = [];
  for (let i = 0; i < numFailedRequests; i++) {
    const fakeRequest = ["GET", `${BASE_URL}/fake`, null, { tags: { name: "Fake_Request" } }];
    requests.push(fakeRequest);
  }

  ENDPOINTS.forEach(endpoint => {
    requests.push(["GET", `${BASE_URL}${endpoint}`, null, { tags: { name: "Stress_Test" } }]);
  });

  const responses = http.batch(requests);

  for (let i = 0; i < responses.length; i++) {
    const res = responses[i];

    if (res.status === 200) {
      requestSucceed.add(1);
    } else {
      requestFail.add(1);
    }
    requestTotal.add(1);
  }
}
