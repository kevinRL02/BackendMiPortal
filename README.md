# Orquestación de Microservicios en Kubernetes para una Tienda Online: Integración de Prometheus, Grafana y RabbitMQ

Este repositorio alberga un proyecto que emplea Kubernetes para la orquestación de contenedores, Helm para la instalación de Prometheus, y diversos microservicios desarrollados en .NET, Python y Java EE. El propósito del proyecto es implementar una tienda online, aprovechando diferentes componentes arquitectónicos.

En esta aplicación, se utilizan un balanceadores de carga y un API Gateway con NGINX Controller para gestionar el tráfico entrante y dirigirlo hacia los microservicios correspondientes. Además, se emplea un bus de eventos con RabbitMQ para facilitar la comunicación entre los microservicios de manera eficiente y escalable.

Cada microservicio cuenta con su propia base de datos, lo que permite una mayor modularidad y flexibilidad en el desarrollo y despliegue de la aplicación. Además, se integra Prometheus y Grafana para la monitorización del clúster Kubernetes, asegurando un seguimiento detallado del rendimiento y la salud del sistema en tiempo real
## Tabla de Contenidos

- [Descripción](#descripción)
- [Arquitectura](#arquitectura)
- [Prerrequisitos](#prerrequisitos)
- [Instalación](#instalación)
  - [Configuración de Kubernetes](#configuración-de-kubernetes)
  - [Despliegue de Prometheus y Grafana con Helm](#despliegue-de-prometheus-y-grafana-con-helm)
  - [Despliegue de Microservicios](#despliegue-de-microservicios)
  - [Configuración de Variables de Entorno](#configuración-de-variables-de-entorno)
- [Uso](#uso)


## Descripción

Este proyecto está diseñado para demostrar cómo se pueden utilizar tecnologías modernas como Kubernetes y Helm para gestionar y monitorizar microservicios. Prometheus se usa para la monitorización y recolección de métricas, lo que permite una observabilidad completa del sistema.

## Arquitectura

La arquitectura del proyecto incluye los siguientes componentes:

- **Kubernetes**: Orquestación de contenedores.
- **Helm**: Gestión de paquetes de Kubernetes.
- **Prometheus**: Monitorización y recolección de métricas.
- **Microservicios**:
  - .NET
  - Python
  - Java EE

## Prerrequisitos

Antes de comenzar, asegúrate de tener instalados los siguientes componentes:

- [Docker](https://www.docker.com/get-started)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Helm](https://helm.sh/docs/intro/install/)
- Un clúster de Kubernetes (puedes usar [Minikube](https://minikube.sigs.k8s.io/docs/start/) para pruebas locales)

## Instalación

### Configuración de Kubernetes

Si no tienes un clúster de Kubernetes, puedes usar Minikube para crear uno localmente:

- minikube start

Despliegue de Prometheus y Grafana con Helm
Primero, agrega el repositorio de Helm para Prometheus:

- helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
- helm repo update

Instala Prometheus usando Helm:

- helm install prometheus prometheus-community/prometheus

Despliegue de Microservicios
Aplica todos los archivos de despliegue YAML de Kubernetes para cada microservicio. Puedes hacerlo con el siguiente comando:

Parametricza prometheus y grafna.

- helm upgrade prometheus prometheus-community/kube-prometheus-stack -f values.yaml 

Sube todos los microservicios a kuberentes

- kubectl apply -f k8s/

Configuración de Variables de Entorno
Para el uso del ApiGateway, asegúrate de mapear la dirección 127.0.0.1 al nombre onlineshop en las variables de entorno. Puedes hacer esto añadiendo la siguiente línea a tu archivo /etc/hosts:

- 127.0.0.1 onlineshop

# Uso
Para acceder a los microservicios y la interfaz de Prometheus, usa los servicios expuestos en tu clúster de Kubernetes. Puedes obtener los detalles de los servicios con el siguiente comando:

- kubectl get services
