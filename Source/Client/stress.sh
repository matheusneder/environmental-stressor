#!/bin/bash

source $(dirname $0)/shared

trap "terminate" SIGINT SIGTERM

function terminate
{
	for pid in ${array[*]}
	do
	    kill -9 $pid > /dev/null 2>&1
	done	

	exit
}

ACTION=$2
PARALLEL_INSTANCES=$1


function usage
{
	echo "Usage:"
	echo "$0 PARALLEL_INSTANCES 'command to execute'"
}

if ! is-valid-integer $PARALLEL_INSTANCES
then
	echo "The first argument must be a valid integer with how many parallel instances you wish to execute." >> /dev/stderr
	usage
	exit 1
fi

if [ "x$ACTION" == "x" ]
then
	echo echo "You must provide the second argument with the command you wish to execute" >> /dev/stderr
	usage
	exit 2
fi

c=0

while [ true ]
do 
	while [ ${#array[*]} -lt $PARALLEL_INSTANCES ]
	do
		$ACTION & 
		array[c]=$!
		let c++
	done
	
	for i in ${!array[*]}
	do
		pid=${array[i]}
		if ! ps | awk '{ print $1 }' | egrep "^${pid}$" > /dev/null
		then 
			unset array[i]
			break
		fi
	done	
done

