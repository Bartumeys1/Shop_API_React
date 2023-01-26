import axios from "axios";

const http = axios.create({

    baseURL: "http://localhost:5043/",
    headers: {
        "Content-type": "application/json"
    }
});

export default http;