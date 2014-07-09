#!/bin/bash
SEPARATOR=".."
RANGE=""
if [ "$1" != "" ]; then
	RANGE="$1$SEPARATOR"
fi
if [ "$2" != "" ]; then
	RANGE+="$2"
fi

git log --pretty=tformat: --numstat $RANGE | gawk '{ add += $1 ; subs += $2 ; loc += $1 - $2 } END { printf "Added lines: %s Removed lines: %s Total lines: %s\n",add,subs,loc }' -
