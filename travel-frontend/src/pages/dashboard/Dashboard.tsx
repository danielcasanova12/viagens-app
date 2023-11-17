import { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { ToolsDetails } from "../../shared/components";
import { UserService} from "../../shared/services/api/user/UserService";
import {  Button, Card, CardContent,  Grid,   Typography } from "@mui/material";
import { IUser } from "../../shared/Interfaces/Interfaces";

export const Dashboard = () => {
	const [users, setUsers] = useState<IUser[]>([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<Error | null>(null);

	useEffect(() => {
		setLoading(true);
		UserService.getAllUsers()
			.then((data) => {
				if (data instanceof Error) {
					setError(data);
				} else {
					setUsers(data);
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
			<Grid container spacing={2}>
				{users.map((user) => (
					<Grid item xs={12} sm={6} md={4} key={user.IdUser}>
						<Card variant="outlined" style={{ height: "100%" }}>
							<CardContent>
								<Typography variant="h6" component="div">
                  Username: {user.username}
								</Typography>
								<Typography color="textSecondary">
                  Email: {user.email}
								</Typography>
								<Button
									variant="contained"
									color="primary"
								>
                  Detalhes
								</Button>
							</CardContent>
						</Card>
					</Grid>
				))}
			</Grid>
		</LayoutBasePage>
	);
	
};