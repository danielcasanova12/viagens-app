# Use uma imagem base do Node.js
FROM node:alpine

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Adicione `/app/node_modules/.bin` ao $PATH
ENV PATH /app/node_modules/.bin:$PATH

# Copie o arquivo package.json e yarn.lock, se disponível, para o diretório de trabalho
COPY package.json yarn.lock ./

# Instale as dependências do projeto
RUN yarn install --silent

# Copie os arquivos do projeto para o diretório de trabalho
COPY . ./

# Inicie o servidor de desenvolvimento
CMD ["yarn", "start"]
