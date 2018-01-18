#!/bin/bash

source $(dirname $0)/shared

if ! is-valid-integer $1
then
	echo "Missing 'megabytes' or the given argument is not a valid integer" >> /dev/stderr
	echo "Usage: "
	echo "$0 10"
	exit 1
fi

perform-action-quietly "/Simulations/HighThroughput/$1"