# Take-home: Make this service production-ready

You have a small .NET HTTP service in [`src/`](src/) and a set of Kubernetes manifests in [`k8s/`](k8s/). As shipped, the service does not run healthy in a cluster.

## Your task

1. **Get it healthy** - the pod should start, stay up, and serve traffic through the Service.
2. **Then make it production-ready.**
3. **Write a short README** explaining every change you made and why.

## What you get

- `src/` - an ASP.NET Core minimal API listening on port `8080`, with `/health/live` and `/health/ready` endpoints. It has a ~30 second warmup before it reports ready.
- `k8s/` - Deployment, Service, ConfigMap, Secret, Ingress, ServiceAccount + RBAC.

## Building the image (optional)

```
docker build -t payment-service:local src/
```

You can point the Deployment at your own registry/tag, or side-load the image into kind/minikube.

## Ground rules

- Target any cluster you like: kind, minikube, or a scratch EKS namespace.
- You may restructure the manifests however you see fit - add, split, or remove files.
- Timebox: 60-90 minutes. If you run out of time, note in your README what you would have done next.

## How we evaluate

We care more about your reasoning than about a fully green cluster. Explain your trade-offs, and proactively call out anything you would harden for production even if it was not strictly required to make the service run.
