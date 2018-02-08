# Environmental Stressor

[![Build Status](https://travis-ci.org/matheusneder/environmental-stressor.svg?branch=master)](https://travis-ci.org/matheusneder/environmental-stressor)

This is an ASP.NET Core web application for test environment (like a server or cluster).

It provides operations to simulate:
  - Long running startup
  - Long running request
  - High CPU usage
  - Memory leak
  - Out of memory
  - High throughput

# Running

Docker image available at: https://hub.docker.com/r/matheusneder/environmental-stressor

```
# docker run matheusneder/environmental-stressor:v1.0-alpha.4
```
