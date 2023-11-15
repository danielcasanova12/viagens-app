import React, { useState } from "react";
import { useAuthContext } from "../../shared/contexts/AuthContext";
import { Box, Typography } from "@material-ui/core";
import {LayoutBasePage} from "../../shared/layouts/LayoutBasePage";
export const Profile = () => {
	const { user } = useAuthContext();
	const [username] = useState(user?.username || "");
	const [image] = useState(user?.image || "");



	return (
		<div style={{ display: "flex", justifyContent: "center"}}>
			<LayoutBasePage title="Welcome to your profile">
				<Box 
					display="flex" 
					flexDirection="column" 
					alignItems="center" 
					justifyContent="center" 
					minHeight="100vh"
					bgcolor="lightblue"
					padding="2rem"
				>
					{user && (
						<Box display="flex" flexDirection="column" alignItems="center" marginTop="2rem">
							<img src={image} alt={username} style={{ width: "200px", height: "200px" }} />
							<Typography variant="h5" color="textSecondary">Nome: {username}</Typography>
							<Typography variant="h5" color="textSecondary">Email: {user?.email}</Typography>
						</Box>
					)}
				</Box>
			</LayoutBasePage>
		</div>

	);
};
