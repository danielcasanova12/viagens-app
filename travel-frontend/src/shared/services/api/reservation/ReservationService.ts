
import { ICreateReservation,IReservationsAll } from "../../../Interfaces/Interfaces";
import { Api } from "../axios-config";

const postReservation = async (reservationDto: ICreateReservation): Promise<ICreateReservation | Error> => {
	try {
		console.log("reservationDto", reservationDto);
		const { data } = await Api.post("/Reservation", reservationDto);

		if (data) {

			console.log("data", data);
			
		}

		return new Error("Erro ao criar a reserva.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao criar a reserva.");
	}
};
const getReservationById = async (id: number): Promise<ICreateReservation | Error> => {
	try {
		const { data } = await Api.get(`/Reservation/${id}`);

		if (data) {
			console.log("data", data);
			return data;
		}

		return new Error("Erro ao obter a reserva.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao obter a reserva.");
	}
};
const getReservationsByUserId = async (userId: number): Promise<IReservationsAll | Error> => {
	try {
		const { data } = await Api.get(`/Reservation/user/${userId}`);

		if (data) {
			console.log("data", data);
			return data;
		}

		return new Error("Erro ao obter as reservas.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao obter as reservas.");
	}
};

export const ReservationService = {
	postReservation,
	getReservationById,
	getReservationsByUserId,
};