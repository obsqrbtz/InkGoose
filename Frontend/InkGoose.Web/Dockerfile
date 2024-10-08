FROM node:20 as build-stage
WORKDIR /build

ARG VITE_API_HOST

COPY Frontend/InkGoose.Web .
RUN npm install
RUN npm run build
 
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /build/dist /usr/share/nginx/html
COPY --from=build-stage /build/nginx/default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]