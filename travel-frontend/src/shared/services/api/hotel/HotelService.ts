import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";
interface Image {
	id: number;
  hotelId: number;
  imageUrl: string;
}
export interface IHotel {
  idHotel: number;
  name: string;
  location: {
    idLocal: number;
    name: string;
    adress: string;
    city: string;
    state: string;
    country: string;
    image?: string;
  };
  starRating: number;
  pricePerNight: number;
  typesRoom: {
    idTypeRoom: number;
    name: string;
    priceDaily: number;
  }[];
	images: Image[];
}

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




export const HotelService = {
	getAllHotels,
};