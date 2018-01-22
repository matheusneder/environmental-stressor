#!/bin/bash

source $(dirname $0)/shared

if ! is-valid-integer $1
then
	echo "Missing argument" >> /dev/stderr
	echo "Usage: "
	echo "$0 [sleepForMilliseconds]"
	exit 1
fi

perform-action-quietly "/Simulations/LongRunning/$1"