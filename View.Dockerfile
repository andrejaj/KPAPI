FROM nginx:alpine

COPY View/default.conf /etc/nginx/conf.d/default.conf
COPY View/view.html /usr/share/nginx/html/view.html

EXPOSE 81
CMD [ "nginx", "-g", "daemon off;" ]
