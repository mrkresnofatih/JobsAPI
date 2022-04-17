# Jobs Application

It's just a to do list with features as such:
1. Get your task list
2. Add new task list
3. Delete specific task (works in the backend, the FE is buggy. You need to logout then login to see the new list after deletion)

Since it is asynchronous, the Job API might take sometime to send the response back to the Front End.
Take into account the proximity (Regions) of your AWS resources/services to speed this up. To put into perspective, I'm running from Indonesia `us-east-1` and it took almost 10 seconds of wait for every request response.

## About

Go to [notion](https://mirror-tang-363.notion.site/Jobs-API-dd097c4356ea4589bb6de71c42441024) for the design plan. It's not much. It's just a practice project after all but it's pretty cool, includes:
1. Asynchronous Communication using AWS SQS.
2. Transaction Response to Client (Front End) using Websockets from Pusher.
3. Centralized Logging & Traceability using Manually Assigned SpanID to AWS CloudWatch for Checking If A Transaction is completed or not.

## Run Job Application Stack with Docker Compose

### Steps

1. Create IAM for AWS SQS with Max. Policy SQSFullAccess
2. Save the Key Pair
3. Create IAM for AWS CLOUDWATCH with Max. Policy CloudWatchFullAccess
4. Save the Key Pair
5. Create 2 Standard (not FIFO) AWS SQS Queues for LoggerQueue & JobsCoreQueue
6. Go to [Pusher](https://pusher.com/), Create an application for Pusher Channels
7. Save Secrets
8. For the CloudWatch LogGroup, just give any name. It's a string.
9. Insert all the variables here. The vars for input uses `<VAR_NAME>` pattern.
10. Then run `docker-compose -f <docker-compose-file-name>.yml up -d`.
11. To take the entire stack down, run `docker-compose -f <docker-compose-file-name>.yml down -v`

```yaml
version: "3.8"
services:
  redis:
    container_name: "redis"
    image: redis:6.2
    ports:
      - "6379:6379"

  authedgateway:
    container_name: "authedgateway"
    image: ghcr.io/mrkresnofatih/ghcr.io/mrkresnofatih/jobsapi-authedgateway:v1.0.5
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - REDIS_HOST=redis
      - REDIS_PORT=6379
      - REDIS_PASS=password
      - SQS_ACCESSKEY=<IAM SQS_ACCESSKEY>
      - SQS_SECRET=<IAM SQS_SECRET>
      - SQS_REGION=<IAM SQS_REGION_CODE e.g. us-east-1>
      - SQS_JOBSCORE_URL=<AWS SQS_JOBSCORE_URL e.g. "https://sqs.us-east-1.amazonaws.com/blabla/sqsname">
      - SQS_LOGGER_URL=<AWS SQS_LOGGER_URL e.g. "https://sqs.us-east-1.amazonaws.com/blabla/sqsname">

  jobscore:
    container_name: "jobscore"
    image: ghcr.io/mrkresnofatih/ghcr.io/mrkresnofatih/jobsapi-jobscore:v1.0.5
    ports:
      - "8081:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - REDIS_HOST=redis
      - REDIS_PORT=6379
      - REDIS_PASS=password
      - SQS_ACCESSKEY=<IAM SQS_ACCESSKEY>
      - SQS_SECRET=<IAM SQS_SECRET>
      - SQS_REGION=<IAM SQS_REGION_CODE e.g. us-east-1>
      - SQS_JOBSCORE_URL=<AWS SQS_JOBSCORE_URL e.g. "https://sqs.us-east-1.amazonaws.com/blabla/sqsname">
      - SQS_LOGGER_URL=<AWS SQS_LOGGER_URL e.g. "https://sqs.us-east-1.amazonaws.com/blabla/sqsname">
      - PUSHER_APP_ID=<PUSHER APP ID>
      - PUSHER_KEY=<PUSHER KEY>
      - PUSHER_SECRET=<PUSHER SECRET>
      - PUSHER_CLUSTER=<PUSHER CLUSTER>

  loggerapp:
    container_name: "loggerapp"
    image: ghcr.io/mrkresnofatih/ghcr.io/mrkresnofatih/jobsapi-loggerapp:v1.0.5
    ports:
      - "8082:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - CLOUDWATCH_ACCESSKEY=<AWS CLOUDWATCH IAM ACCESSKEY>
      - CLOUDWATCH_SECRET=<AWS CLOUDWATCH IAM SECRET>
      - CLOUDWATCH_AWSREGION=<AWS CLOUDWATCH REGION>
      - CLOUDWATCH_LOGGROUP=<AWS CLOUDWATCH LOG GROUP>
      - SQS_ACCESSKEY=<IAM SQS_ACCESSKEY>
      - SQS_SECRET=<IAM SQS_SECRET>
      - SQS_REGION=<IAM SQS_REGION_CODE e.g. us-east-1>
      - SQS_LOGGER_URL=<AWS SQS_LOGGER_URL e.g. "https://sqs.us-east-1.amazonaws.com/blabla/sqsname">

  jobsapp:
    container_name: "jobsapp"
    image: ghcr.io/mrkresnofatih/ghcr.io/mrkresnofatih/jobsapi-fe-app:v1.0.1
    ports:
      - "4000:3000"
    environment:
      - REACT_APP_PUSHER_APP_KEY=<PUSHER KEY>
      - REACT_APP_PUSHER_CLUSTER=<PUSHER CLUSTER>
      - REACT_APP_API_URL=http://localhost:8080
```

### Links

1. [Front End](https://github.com/users/mrkresnofatih/packages/container/package/ghcr.io%2Fmrkresnofatih%2Fjobsapi-fe-app)
2. [Authed Gateway](https://github.com/users/mrkresnofatih/packages/container/package/ghcr.io%2Fmrkresnofatih%2Fjobsapi-authedgateway)
3. [JobsCore](https://github.com/users/mrkresnofatih/packages/container/package/ghcr.io%2Fmrkresnofatih%2Fjobsapi-jobscore)
4. [LoggerApp](https://github.com/users/mrkresnofatih/packages/container/package/ghcr.io%2Fmrkresnofatih%2Fjobsapi-loggerapp)