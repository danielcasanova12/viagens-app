import { Box, Button, Paper, TextField, useTheme} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";

interface IToolbarProps {
  children: React.ReactNode;
}
export const Toolbar:React.FC<IToolbarProps> = ({children}) => {
	const theme = useTheme();
  
	return (
		<Box
			height={theme.spacing(5)}
			marginX={1}
			padding={1}
			paddingX={2}
			display={"flex"}
			gap={1}
			alignItems="center"
			component={Paper}
		>
			<TextField
				size='small'
				placeholder='Search...'
				variant='outlined'
			/>
			
			<Box 
				flex={1}
				display={"flex"}
				justifyContent={"end"}
        
			>
				<Button
					variant='contained'
					color='primary'
					disableElevation
					endIcon={<AddIcon />}
				>Novo
				</Button>
				{children}
			</Box>
		</Box>
	);
};