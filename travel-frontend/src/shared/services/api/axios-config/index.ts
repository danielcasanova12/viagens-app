import axios from "axios";
import { errorInterceptor, responseInterceptor } from "./interceptors";

const api = axios.create({
	baseURL: "Environment.URL_DEFAULT",
});

api.interceptors.response.use(
	(response) => responseInterceptor(response),
	(error) => errorInterceptor(error)
);

export {api};