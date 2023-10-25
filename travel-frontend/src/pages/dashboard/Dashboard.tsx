import React, { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { ToolsDetails } from "../../shared/components";
import { UserService, IUser } from "../../shared/services/api/user/UserService";

export const Dashboard = () => {
	const [users, setUsers] = useState<IUser[]>([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<Error | null>(null);

	useEffect(() => {
		setLoading(true);
		UserService.getAllUsers()
				if (data instanceof Error) {
					setError(data);
				} else {
					setUsers(data);
					console.log(data[0].idUser);
					console.log("Dados recebidos com sucesso!");
				}
			})
			.catch((error) => {
				setError(error);
			})
			.finally(() => {
				setLoading(false);
			});
	}, []);

	return (
		<LayoutBasePage title="Dashboard" toolbar={<ToolsDetails />}>
			{loading && <p>Loading...</p>}
			{error && <p>{error.message}</p>}
			{users.map((user) => (
				<div key={user.idUser}>
					<p>Username: {user.username}</p>
					<p>Email: {user.email}</p>
					{/* outras propriedades do usuário */}
				</div>
			))}
		</LayoutBasePage>
	);
};