

const gerAllUsers = async () => {
	const response = await axios.get("/users");
	return response.data;
};

const gerAllUsersById = async () => {
	const response = await axios.get("/users");
	return response.data;
};

const create = async () => {
	const response = await axios.get("/users");
	return response.data;
};
const updateById = async () => {
	const response = await axios.get("/users");
	return response.data;
};
const deleteById = async () => {
	const response = await axios.get("/users");
	return response.data;
};



export const UserService = {
	gerAllUsers,
	gerAllUsersById,
	create,
	updateById,
	deleteById
};