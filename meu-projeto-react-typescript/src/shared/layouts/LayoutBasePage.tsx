import { Box, Theme, Typography,  useMediaQuery,  useTheme } from "@mui/material";
import { ReactNode } from "react";

interface ILayoutBaseDePageProps {
  children: React.ReactNode;
  title: string;
	toolbar?: ReactNode;

}
export const LayoutBasePage: React.FC<ILayoutBaseDePageProps> = ({ children, title, toolbar }) => {
	const smDown = useMediaQuery((theme: Theme) => theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery((theme: Theme) => theme.breakpoints.down("md"));
	const theme = useTheme();


	return (
		<Box height="100%" display="flex" flexDirection="column" gap={1}>
			<Box padding={1} display="flex" alignItems="center" gap={1} height={theme.spacing(smDown ? 6 : mdDown ? 8 : 12)}>
				

				<Typography
					overflow="hidden"
					whiteSpace="nowrap"
					textOverflow="ellipses"
					variant={smDown ? "h5" : mdDown ? "h4" : "h3"}
				>
					{title}
				</Typography>
			</Box>

			{toolbar && (
				<Box>
					{toolbar}
				</Box>
			)}

			<Box flex={1} overflow="auto">
				{children}
			</Box>
		</Box>
	);
};