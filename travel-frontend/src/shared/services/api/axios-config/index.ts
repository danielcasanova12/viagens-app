import axios from "axios";
import { errorInterceptor, responseInterceptor } from "./interceptors";
import { Environment } from "../../../environment/Environments";

const Api = axios.create({
	baseURL:  Environment.URL_DEFAULT,
});

Api.interceptors.response.use(
	(response) => responseInterceptor(response),
	(error) => errorInterceptor(error)
);

export {Api};