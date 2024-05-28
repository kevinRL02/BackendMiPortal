K6_PROMETHEUS_RW_SERVER_URL=http://localhost:9090/api/v1/write k6 run -e API_URL="http://onlineshop.com" -e VUS="30,10,5,0" -e DURATION="50s,12s,5s,10s" -e ENDPOINTS="/api/users,/api/c/shoppingcarts,/api/products" -e DELIMITER=","  --tag testid=nanoe -o experimental-prometheus-rw load_test.js

