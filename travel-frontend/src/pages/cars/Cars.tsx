import React from "react";
import { useAuthContext } from "../../shared/contexts/AuthContext"; // Altere para useAuthContext

export const Carros = () => {
	const { user } = useAuthContext(); // Acesse o usuário do contexto de autenticação

	return (
		<div>
			<h1>Página de Carros</h1>
			{user && (
				<div>
					<h2>Usuário Logado:</h2>
					<img src={user.image} alt={user.username} /> 
					<p>Nome de Usuário: {user.username}</p>
				</div>
			)}
		</div>
	);
};