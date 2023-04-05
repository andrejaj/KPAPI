FROM nginx:alpine

COPY View/nginx.conf /etc/nginx/conf.d/default.conf
COPY View/index.html /usr/share/nginx/html/index.html

CMD [ "nginx", "-g", "daemon off;" ]
