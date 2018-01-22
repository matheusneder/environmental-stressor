#!/bin/bash

source $(dirname $0)/shared

if ! is-valid-integer $1 || ! is-valid-integer $2
then
	echo "Missing arguments" >> /dev/stderr
	echo "Usage: "
	echo "$0 [megabytes] [sleepForMilliseconds]"
	exit 1
fi

perform-action-quietly "/Simulations/HighThroughput/$1/$2"