
import { IHotel } from "../../../Interfaces/Interfaces";
import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";


type THotelsCount = {
  data: IHotel[];
  totalCount: number;
}

const getAllHotels = async (searchValue: string, pageNumber = 1): Promise<THotelsCount[] | Error> => {
	try {
		const urlRelative = `/Hotels?pageNumber=${pageNumber}&pageSize=${Environment.LIMIT_DEFAULT}&searchValue=${searchValue}`;
		const { data } = await Api.get(urlRelative); // Não é mais necessário pegar os headers

		if (data) {
			return [{
				data: data.hotels, // Ajuste para pegar os hotéis de data.Hotels
				totalCount: data.totalHotels, // Ajuste para pegar o total de hotéis de data.TotalHotels
			}];
		}

		return new Error("Erro ao listar os registros.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao listar os registros.");
	}
};

const getHotelById = async (id: number): Promise<IHotel | Error> => {
	try {
		const urlRelative = `/Hotels/${id}`;
		const { data } = await Api.get(urlRelative);

		if (data) {
			console.log(data);
			return data;
		}

		return new Error("Erro ao buscar o hotel.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao buscar o hotel.");
	}
};


export const HotelService = {
	getAllHotels,
	getHotelById,
};