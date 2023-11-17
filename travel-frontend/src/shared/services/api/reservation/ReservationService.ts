
import { ICreateReservation,IImage,IReservation,IReservationsAll } from "../../../Interfaces/Interfaces";
import { Api } from "../axios-config";


const getReservationById = async (id: number): Promise<ICreateReservation | Error> => {
	try {
		const { data } = await Api.get(`/Reservation/${id}`);

		if (data) {
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
			const reservations = data.reservations.map((reservation: IReservation) => ({
				idReservation: reservation.idReservation,
				userId: reservation.userId,
				checkInDate: reservation.checkInDate,
				checkOutDate: reservation.checkOutDate,
				reservedHotel: {
					idHotel: reservation.reservedHotel?.idHotel,
					name: reservation.reservedHotel?.name,
					location: reservation.reservedHotel?.location,
					starRating: reservation.reservedHotel?.starRating,
					pricePerNight: reservation.reservedHotel?.pricePerNight,
					images: reservation.reservedHotel?.images.map((image: IImage) => ({
						id: image.id,
						hotelId: image.hotelId,
						imageUrl: image.imageUrl,
					})),
				},
			}));

			return {
				reservations,
				totalReservations: data.totalReservations,
			};
		}

		return new Error("Erro ao obter as reservas.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao obter as reservas.");
	}
};
const postReservation = async (reservationDto: ICreateReservation): Promise<ICreateReservation | Error> => {
	try {
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

export const deleteReservation = async (id: number): Promise<void> => {
	try {
		await Api.delete(`/Reservation/${id}`);
	} catch (error) {
		console.error(error);
		throw new Error((error as { message: string }).message || "Error deleting the reservation.");
	}
};

export const ReservationService = {
	getReservationById,
	getReservationsByUserId,
	postReservation,
	deleteReservation,
};