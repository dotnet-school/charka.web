TODO

- [ ] replace image name in template
- [ ] publish k8 scripts in artifacts
- [ ] Create environment
- [ ] Auto release YAML task for DEV environment
- [ ] Release task for QA/PROD environment

Resources: 

-  https://docs.microsoft.com/en-in/azure/aks/kubernetes-walkthrough



### Setup project

- clone this repo  https://github.com/dotnet-school/dotnet-aks

- Install azure CLI : https://docs.microsoft.com/en-us/cli/azure/install-azure-cli

- Create azure resources (one time setup)

  ```bash
  # login using azure cli
  az login
  
  RESOURCE_GROUP=devops-aks
  CLUSTER_NAME=devops-aks
  REGION=westeurope
  REGISTRY_NAME=devopsaksregistry
  
  # Create resource
  az group create --name $RESOURCE_GROUP --location $REGION
  
  # Create cluster on AKS with 1 node
  az aks create --resource-group $RESOURCE_GROUP \
  --name $CLUSTER_NAME \
  --node-count 1 \
  --enable-addons monitoring \
  --generate-ssh-keys
  
  # Allow kubectul to connect and manage our AKS clustuer
  az aks get-credentials \
  --resource-group $RESOURCE_GROUP \
  --name $CLUSTER_NAME
  
  # Merged "aks-dotnet" as current context in /Users/dawn/.kube/config
  kubectl get nodes
    # NAME                                STATUS   ROLES   AGE     VERSION
    # aks-nodepool1-36600731-vmss000000   Ready    agent   2m58s   v1.16.10
    
  # Create azure container registry
  az acr create \
  --resource-group $RESOURCE_GROUP \
  --name $REGISTRY_NAME \
  --sku Standard \
  --location $REGION 
  
  ```



### Create an environment

- Now that we have a kubernetes cluster, we will now create a new environment: 

  ![image-20200926225427043](/Users/dawn/projects/dotnet-school/devops-aks/docs/images/environments.png)



- Add kubernetes resource to the environment 

  ![image-20200926225628492](/Users/dawn/projects/dotnet-school/devops-aks/docs/images/k8-env.png)


- Select our kubernetes cluster  and create enrironment : 

  ![image-20200926225726576](/Users/dawn/projects/dotnet-school/devops-aks/docs/images/k8-env-cluster.png)



- Create pipeline from template : 

  ![image-20200926231033295](/Users/dawn/projects/dotnet-school/devops-aks/docs/images/pipeline-template.png)



- 




- Get public IP of load balancer : 

  ```yaml
  kubectl get service/todo-app-service
  # NAME               TYPE           CLUSTER-IP    EXTERNAL-IP    
  # todo-app-service   LoadBalancer   10.0.22.204   20.56.237.117   
  ```

  - now open the public IP with http (we havent configured https yet) : http://20.56.237.117/



- Delete the resource

  ```bash
  # delete cluster
  az group delete --name $RESOURCE_GROUP --yes --no-wait
  ```

  



- Creating a release pipeline