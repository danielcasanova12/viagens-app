import { AxiosError } from "axios";


export const errorInterceptor = (error: AxiosError) => {
	if(error.response?.status == 401){
		return Promise.reject(new Error("Not authorized"));
	}

	return Promise.reject(error);
};