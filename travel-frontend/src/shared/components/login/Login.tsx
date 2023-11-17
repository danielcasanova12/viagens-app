import { useState } from "react";
import { Box, Button, Card, CardActions, CardContent, CircularProgress, TextField, Typography, IconButton, InputAdornment } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import * as yup from "yup";

import { useAuthContext } from "../../contexts";
import { UserService} from "../../services/api";
import { IUsers } from "../../Interfaces/Interfaces";

const loginSchema = yup.object().shape({
	email: yup.string().email("Por favor, insira um email válido.").required("O campo de email é obrigatório."),
	password: yup.string().required("O campo de senha é obrigatório.").min(5, "A senha deve ter pelo menos 5 caracteres."),
});

const accountSchema = yup.object().shape({
	username: yup.string().required("O campo de nome de usuário é obrigatório."),
	email: yup.string().email("Por favor, insira um email válido.").required("O campo de email é obrigatório."),
	password: yup.string().required("O campo de senha é obrigatório.").min(5, "A senha deve ter pelo menos 5 caracteres."),
	image: yup.string().required("O campo de imagem é obrigatório."),
	typePermission: yup.string().required("O campo de tipo de permissão é obrigatório."),
});

interface ILoginProps {
  children: React.ReactNode;
}

export const Login: React.FC<ILoginProps> = ({ children }) => {
	const { isAuthenticated, login } = useAuthContext();

	const [isLoading, setIsLoading] = useState(false);
	const [isCreatingAccount, setIsCreatingAccount] = useState(false);
	const [showPassword, setShowPassword] = useState(false);
	const [loginError, setLoginError] = useState("");
	const [username, setUsername] = useState("");
	const [usernameError, setUsernameError] = useState("");
	const [email, setEmail] = useState("");
	const [emailError, setEmailError] = useState("");
	const [password, setPassword] = useState("");
	const [passwordError, setPasswordError] = useState("");
	const [image, setImage] = useState("");
	const [imageError, setImageError] = useState("");
	const [typePermission, setTypePermission] = useState("");
	const [typePermissionError, setTypePermissionError] = useState("");

	const handleShowPassword = () => setShowPassword(!showPassword);

	const handleSubmit = async () => {
		setIsLoading(true);
	
		try {
			if (isCreatingAccount) {
				const dadosValidados = await accountSchema.validate({ username, email, password, image, typePermission }, { abortEarly: false });
				// Limpa o erro de email se o email for válido
				setEmailError("");
				
				const user: IUsers = {
					username: dadosValidados.username,
					email: dadosValidados.email,
					password: dadosValidados.password,
					image: dadosValidados.image,
					typePermission: dadosValidados.typePermission,
					reservations: [], // Add the reservations here
				};
				
				const result = await UserService.createUser(user);
	
				// If account creation is successful, set isCreatingAccount to false and call handleSubmit again
				if (result) {
					setIsCreatingAccount(false);
					handleSubmit();
				}
			} else {
				const dadosValidados = await loginSchema.validate({ email, password }, { abortEarly: false });
				// Limpa o erro de email se o email for válido
				
				
				const result = await login(dadosValidados.email, dadosValidados.password);
				if (result == "Request failed with status code 400") {
					setLoginError("O login falhou. Por favor, tente novamente.");
				}
			}
	
			setIsLoading(false);
		} catch (errors) {
			setIsLoading(false);
	
			if (errors instanceof yup.ValidationError) {
				errors.inner.forEach(error => {
					switch (error.path) {
					case "username":
						setUsernameError(error.message);
						break;
					case "email":
						setEmailError(error.message);
						break;
					case "password":
						setPasswordError(error.message);
						break;
					case "image":
						setImageError(error.message);
						break;
					case "typePermission":
						setTypePermissionError(error.message);
						break;
					default:
						break;
					}
				});
			} 
		}
	};

	if (isAuthenticated) return (
		<>{children}</>
	);

	return (
		<Box width='100vw' height='100vh' display='flex' alignItems='center' justifyContent='center'>

			<Card>
				<CardContent>
					<Box display='flex' flexDirection='column' gap={2} width={250}>
						<Typography variant='h6' align='center'>{isCreatingAccount ? "Crie sua conta" : "Identifique-se"}</Typography>

						{isCreatingAccount && (
							<TextField
								fullWidth
								label='Nome de usuário'
								value={username}
								disabled={isLoading}
								onChange={e => setUsername(e.target.value)}
								helperText={usernameError}
								error={!!usernameError}
							/>
						)}

						<TextField
							fullWidth
							type='email'
							label='Email'
							value={email}
							disabled={isLoading}
							onChange={e => setEmail(e.target.value)}
							helperText={emailError}
							error={!!emailError}
						/>
						

						<TextField
							fullWidth
							label='Senha'
							type={showPassword ? "text" : "password"}
							value={password}
							disabled={isLoading}
							onChange={e => setPassword(e.target.value)}
							InputProps={{
								endAdornment: (
									<InputAdornment position="end">
										<IconButton
											aria-label="toggle password visibility"
											onClick={handleShowPassword}
										>
											{showPassword ? <VisibilityOff /> : <Visibility />}
										</IconButton>
									</InputAdornment>
								),
							}}
							helperText={passwordError}
							error={!!passwordError}
						/>
						
						{isCreatingAccount && (
							<>
								<TextField
									fullWidth
									label='Imagem'
									value={image}
									disabled={isLoading}
									onChange={e => setImage(e.target.value)}
									helperText={imageError}
									error={!!imageError}
								/>

								<TextField
									fullWidth
									label='Tipo de permissão'
									value={typePermission}
									disabled={isLoading}
									onChange={e => setTypePermission(e.target.value)}
									helperText={typePermissionError}
									error={!!typePermissionError}
								/>
							</>
						)}
						
					</Box>
				</CardContent>
				{(!isCreatingAccount && loginError &&
					<p style={{ color: "red" }}>{loginError}</p>
				)}
				<CardActions>
					<Box width='100%' display='flex' justifyContent='center'>
						

						<Button
							variant='contained'
							disabled={isLoading}
							onClick={handleSubmit}
							endIcon={isLoading ? <CircularProgress variant='indeterminate' color='inherit' size={20} /> : undefined}
						>
							{isCreatingAccount ? "Criar Conta" : "Entrar"}
						</Button>

						<Button
							color='secondary'
							disabled={isLoading}
							onClick={() => setIsCreatingAccount(!isCreatingAccount)}
						>
							{isCreatingAccount ? "Já tem uma conta" : "Criar Conta Gratis"}
						</Button>

					</Box>
				</CardActions>
			</Card>
		</Box>
	);
};
