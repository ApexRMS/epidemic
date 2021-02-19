---
layout: default
title: Home
nav_order: 1
description: "Landing page for the Package"
permalink: /
---

# **Epidemic** Syncrosim Package
#### Current version: 1.0.3
#### [Github Repository](https://github.com/ApexRMS/epidemic)
## Model of epidemic infections and deaths
### *Epidemic* is an open-source SyncroSim Base Package that provides a general framework for developing scenario-based stochastic models of future daily epidemic infections and deaths.

<img align="right" style="padding: 10px" width="370" src="assets/images/epidemic-screen-cap.png">

The framework takes as input a time series of actual daily reported deaths, along with a user-supplied fatality rate and infection period, and back-calculates the number of infections that gave rise to the recorded deaths. Based on these presumed infections, the model then projects future infections forward in time based on an exogenously estimated time series of future daily infection growth rates.

At present **Epidemic** allows users to choose from one of two infection growth forms: exponential and logistic. For the logistic growth model, the framework bounds the upper level of possible infections through the specification of a population size and attack rate. Other model growth forms could easily be added in the future. All model projections use the built-in Monte Carlo simulation engine of SyncroSim – as a result model inputs are generally specified as space and time-varying distributions, leading to model projections that include estimates of uncertainty and spatiotemporal variability.

The modeling framework behind epidemic is general enough that it can be applied to any jurisdiction and any disease. Furthermore, because it has been developed using the SyncroSim simulation engine, it is straightforward for users to explore and compare alternative future “what-if” scenarios regarding possible changes in model inputs that might result from changes in public health measures over time.

### Requirements

This package requires Syncrosim [version 2.2.27](https://syncrosim.com/download/).

### How to install

Open SyncroSim and select **File -> Packages… -> Install…**, then select the **epidemic** package and click OK.

Alternatively, download [the latest release from Github](https://github.com/ApexRMS/epidemic/releases/tag/1.0.3). Open Syncrosim and navigate to packages, then install from file.
