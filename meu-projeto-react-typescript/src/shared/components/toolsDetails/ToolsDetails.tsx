import { Box, Button,  Divider,Paper, Skeleton, useTheme } from "@mui/material";
import SaveIcon from "@mui/icons-material/Save";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";

interface IToolsDetailsProps {
  textButtonNew?: string;
  hideButtonNew?: boolean;
  hideButtonBack?: boolean;
  hideButtonSave?: boolean;
  hideButtonDelete?: boolean;
  hideButtonSaveAndBack?: boolean;


  hideButtonNewLoading?: boolean;
  hideButtonBackLoading?: boolean;
  hideButtonSaveLoading?: boolean;
  hideButtonDeleteLoading?: boolean;
  hideButtonSaveAndBackLoading?: boolean;

  onclickNewButton?: () => void;
  onclickBackButton?: () => void;
  onclickSaveButton?: () => void;
  onclickDeleteButton?: () => void;
  onclickSaveAndBackButton?: () => void;
}

export const ToolsDetails: React.FC<IToolsDetailsProps> = ({
	textButtonNew = "Nova",
	hideButtonNew = true ,
	hideButtonBack = true ,
	hideButtonSave = true ,
	hideButtonDelete = true ,
	hideButtonSaveAndBack = false ,

	hideButtonNewLoading = false ,
	hideButtonBackLoading = false ,
	hideButtonSaveLoading = false ,
	hideButtonDeleteLoading = false ,
	hideButtonSaveAndBackLoading = false ,

	onclickNewButton,
	onclickBackButton,
	onclickSaveButton,
	onclickDeleteButton,
	onclickSaveAndBackButton,
}) => {
	const theme = useTheme();
	return (
		<Box
		  gap={1}
		  marginX={1}
		  padding={1}
		  paddingX={2}
		  display="flex"
		  alignItems="center"
		  height={theme.spacing(5)}
		  component={Paper}
		>
		  {(hideButtonSave && !hideButtonSaveLoading) && (
				<Button
			  color='primary'
			  disableElevation
			  variant='contained'
			  onClick={onclickSaveButton}
			  startIcon={<SaveIcon/>}
				>Salvar</Button>
		  )}
	
		  {hideButtonSaveLoading && (
				<Skeleton width={110} height={60} />
		  )}
	
		  {(hideButtonSaveAndBack && !hideButtonSaveAndBackLoading) && (
				<Button
			  color='primary'
			  disableElevation
			  variant='outlined'
			  onClick={onclickSaveAndBackButton}
			  startIcon={<SaveIcon/>}
				>Salvar e voltar</Button>
		  )}
	
		  {hideButtonSaveAndBackLoading && (
				<Skeleton width={180} height={60} />
		  )}
	
		  {(hideButtonDelete && !hideButtonDeleteLoading) && (
				<Button
			  color='primary'
			  disableElevation
			  variant='outlined'
			  onClick={onclickDeleteButton}
			  startIcon={<DeleteIcon/>}
				>Apagar</Button>
		  )}
	
		  {hideButtonDeleteLoading && (
				<Skeleton width={110} height={60} />
		  )}
	
		  {(hideButtonNew && !hideButtonNewLoading) && (
				<Button
			  color='primary'
			  disableElevation
			  variant='outlined'
			  onClick={onclickNewButton}
			  startIcon={<AddIcon/>}
				>{textButtonNew}</Button>
		  )}
	
		  {hideButtonNewLoading && (
				<Skeleton width={110} height={60} />
		  )}
	
		  <Divider variant='middle' orientation='vertical' />
	
		  {(hideButtonBack && !hideButtonBackLoading) && (
				<Button
			  color='primary'
			  disableElevation
			  variant='outlined'
			  onClick={onclickBackButton}
			  startIcon={<ArrowBackIcon/>}
				>Voltar</Button>
		  )}
	
		  {hideButtonBackLoading && (
				<Skeleton width={110} height={60} />
		  )}
		</Box>
	  );
};