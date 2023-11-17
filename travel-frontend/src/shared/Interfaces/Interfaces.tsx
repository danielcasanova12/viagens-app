export interface IReservation {
	IdReservation: number;
  UserId: number;
	checkInDate?: string;
  checkOutDate?: string;
	ReservedHotel?: IHotel
}
export interface IReservationsAll  {
  reservations: IReservation[];
	totalReservations: number;
}

export interface ICreateReservation {
  UserId: number;
	checkInDate?: string;
  checkOutDate?: string;
	ReservedHotel?: IHotel
}
export interface Image {
	id: number;
  hotelId: number;
  imageUrl: string;
}
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
    image?: string;
  };
  starRating: number;
  pricePerNight: number;
	images: Image[];
}

export interface IUser {
	IdUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: string;
  Reservations?: IReservation[]; // Add this line
}
export interface IUsers {
    username: string;
    email : string;
    password: string;
    image: string;
    typePermission: string;
    Reservations?: IReservation[]; // Add this line
}
export interface IAuth {
    accessToken: string;
    user: IUser; // Adicione esta linha
}
