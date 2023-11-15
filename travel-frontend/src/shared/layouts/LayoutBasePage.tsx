import { Box, Typography, useMediaQuery, useTheme } from "@mui/material";
import React from "react";

interface ILayoutBaseDePageProps {
  children: React.ReactNode;
  title: string;
  toolbar?: React.ReactNode;
}

export const LayoutBasePage: React.FC<ILayoutBaseDePageProps> = ({ children, title, toolbar }) => {
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));

	return (
		<Box display="flex" flexDirection="column" gap={1}>
			<Box padding={1} display="flex" alignItems="center" gap={1} height={theme.spacing(smDown ? 6 : 8)}>
				<Typography
					overflow="hidden"
					whiteSpace="nowrap"
					textOverflow="ellipses"
					variant={smDown ? "h5" : "h4"}
				>
					{title}
				</Typography>
			</Box>

			{toolbar && (
				<Box>
					{toolbar}
				</Box>
			)}

			<Box flex={1} overflow="auto" sx={{ width: "100%", maxWidth: "100%" }}>
				{children}
			</Box>
		</Box>
	);
};
