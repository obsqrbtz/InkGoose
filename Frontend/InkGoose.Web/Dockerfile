FROM node:20 as build-stage
WORKDIR /build
# EXPOSE 5173

COPY Frontend/InkGoose.Web .
RUN npm install
RUN npm run build
 
# FROM node:20 as production-stage
# WORKDIR /app
# COPY  --from=build-stage /build/package.json ./
# COPY --from=build-stage /build/dist/ dist/
# RUN npm install --omit=dev
# RUN rm -rf build

FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /build/dist /usr/share/nginx/html
COPY --from=build-stage /build/nginx/default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]