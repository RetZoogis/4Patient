# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM node:14 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . ./
RUN npx ng build

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:1.20
WORKDIR /usr/share/nginx/html
RUN rm -rf *
#Copy default nginx configuration
COPY nginx-custom.conf /etc/nginx/conf.d/default.conf
#Copy ci-dashboard-dist
COPY --from=build /app/dist/* ./

