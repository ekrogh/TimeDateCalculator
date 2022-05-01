set -x #echo on
set -v #echo on
find . -path ./mnt -prune -o -name $1 -print
