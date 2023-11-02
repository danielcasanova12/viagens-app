import { Api } from "../axios-config";

export interface IHotel {
  idHotel: number;
  name: string;
  location?: {
    idLocal: number;
    name: string;
    adress: string;
    city: string;
    state: string;
    country: string;
    image: string;
  };
  starRating: number;
  pricePerNight: number;
  typesRoom: {
    idTypeRoom: number;
    name: string;
    priceDaily: number;
  }[];
  image: string;
}



const getAllHotels = async (): Promise<IHotel[] | Error> => {
	try {
		const urlRelative = "/Hotels";
		const data = await Api.get(urlRelative); 
		return data.data;
	} catch (error) {
		console.error(error);
		return new Error("Erro ao listar os registros.");
	}
};


export const HotelService = {
	getAllHotels,
};