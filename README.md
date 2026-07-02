# Take-home: Make this service production-ready

You have a small .NET HTTP service in [`src/`](src/) and a set of Kubernetes manifests in [`k8s/`](k8s/). As shipped, the service does not run healthy in a cluster.

## Your task

1. **Get it healthy** - the pod should start, stay up, and serve traffic through the Service.
2. **Then make it production-ready.**
3. **Write a short README** explaining every change you made and why.

## What you get

- `src/` - an ASP.NET Core minimal API listening on port `8080`, with `/health/live` and `/health/ready` endpoints. It has a ~30 second warmup before it reports ready.
- `k8s/` - Deployment, Service, ConfigMap, Secret, Ingress, ServiceAccount + RBAC.

## Scope

**In scope** - this is what the exercise is about:

- Diagnosing why the workload does not come up healthy and fixing the Kubernetes manifests.
- Hardening the manifests for production: reliability, security, resource management, and availability.
- Clear written reasoning for every change (the README you produce).

**Out of scope** - you do not need to do these, and we are not grading them:

- Changing the application source code in `src/`. Treat the service as a fixed black box; the fixes live in the manifests. (If you believe an app change is warranted, note it in your README rather than implementing it.)
- Building a CI/CD pipeline, writing Helm/Kustomize/Terraform, or setting up a real ingress controller, DNS, or TLS certificates. Mentioning how you *would* is welcome; implementing it is not required.
- Load testing, dashboards, or wiring up real external secret stores. Referencing the right approach is enough.

**Assumptions you can make:**

- A working cluster and `kubectl` context already exist.
- The container image can be built from `src/` (or substitute any image that serves the same endpoints).
- You may add, split, remove, or restructure manifest files freely.

## Building the image (optional)

```
docker build -t payment-service:local src/
```

You can point the Deployment at your own registry/tag, or side-load the image into kind/minikube.

## Ground rules

- Target any cluster you like: kind, minikube, or a scratch EKS namespace.
- You may restructure the manifests however you see fit - add, split, or remove files.
- Timebox: 60-90 minutes. If you run out of time, note in your README what you would have done next.

## What we expect

We care more about your reasoning than about a fully green cluster. A strong submission shows judgement, not just a working YAML.

We are looking for signal across these areas:

- **Correct diagnosis** - you identify *why* the workload is unhealthy and fix the root cause, not the symptom. A short note on how you diagnosed each issue (what you looked at, what the events/logs told you) is valuable.
- **Reliability** - the service starts, stays up, and can actually be reached through the Service. Health checking is configured deliberately, not copy-pasted.
- **Security** - you spot and fix anything that should not be exposed or over-permissioned, and you know the difference between "hidden" and "secure."
- **Resource management** - the pod schedules predictably and behaves sensibly under pressure; you can reason about the trade-offs you chose.
- **Availability** - you think about what happens when a node or pod dies, and you call it out even if it was not strictly required to make the service run.
- **Communication** - your README is concise and explains *every* change and the reasoning behind it. Trade-offs and "what I'd do next with more time" are a plus.

### What "done" looks like

- The Deployment rolls out and pods stay `Ready` (no crash loops).
- Traffic reaches the pods through the Service.
- Your README walks through each change and why you made it.
- You have flagged anything you would improve for production, even if left unimplemented.

There is no single "correct" answer - we are interested in how you think, what you prioritise in a timebox, and how you communicate it.
