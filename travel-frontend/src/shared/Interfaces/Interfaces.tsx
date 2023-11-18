export interface IReservation {
	idReservation: number;
  userId: number;
	checkInDate?: string;
  checkOutDate?: string;
	reservedHotel?: IHotel
}
export interface IReservationsAll  {
  reservations: IReservation[];
	totalReservations: number;
}
export interface IImage {
	id: number;
  hotelId: number;
  imageUrl: string;
}
export interface IHotel {
  idHotel: number;
  name: string;
  location?: ILocation;
  starRating: number;
  pricePerNight: number;
	images: IImage[];
}
export interface ILocation{
    idLocal: number;
    name: string;
    adress: string;
    city: string;
    state: string;
    country: string;
    image?: string;
}

export interface ICreateReservation {
  UserId: number;
	checkInDate?: string;
  checkOutDate?: string;
	reservedHotel?: IHotel
}


export interface IUser {
	IdUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: string;
  reservations?: IReservation[]; // Add this line
}
export interface IUsers {
    username: string;
    email : string;
    password: string;
    image: string;
    typePermission: string;
    reservations?: IReservation[]; // Add this line
}
export interface IAuth {
    accessToken: string;
    user: IUser; // Adicione esta linha
}
export interface ICarRental
{
    IdCarRental : number;
    Company : string;
    Model : string;
    PricePerDay: number;
    PickupLocation : ILocation;
}
