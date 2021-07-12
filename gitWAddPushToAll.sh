remotes=$(git.exe remote show)
echo remotes=$remotes

buffaloThere="false"
githubThere="false"
misoftThere="false"
originThere="false"
timcapsThere="false"

for rmt in $remotes;
do
	FetchPushsInRmt=$(git.exe remote show $rmt)

	if [[ $FetchPushsInRmt = *"buffalo"* ]] || [[ $FetchPushsInRmt = *"BUFFALO"* ]]
	then
		buffaloThere="true" ;
	fi
	if [[ $FetchPushsInRmt = *"github"* ]] || [[ $FetchPushsInRmt = *"gitHub"* ]]
	then
		githubThere="true"
	fi
	if [[ $FetchPushsInRmt = *"misoft"* ]] || [[ $FetchPushsInRmt = *"miSoft"* ]] || [[ $FetchPushsInRmt = *"azure"* ]]
	then
		misoftThere="true"
	fi
	if [[ $FetchPushsInRmt = *"origin"* ]]
	then
		originThere="true"
	fi
	if [[ $FetchPushsInRmt = *"tim"* ]]
	then
		timcapsThere="true"
	fi

done

DIRNAME=$(pwd)
dir1=$(basename "$(dirname "$DIRNAME")")
dir2=$(basename "$DIRNAME")
if [[ $dir1 = "projects" ]]
then
    dir1=""
else
    dir1=$dir1/
fi
echo dir1   = $dir1
echo dir2   = $dir2
echo enddir = $dir1$dir2

echo buffaloThere=$buffaloThere
echo githubThere=$githubThere
echo misoftThere=$misoftThere
echo originThere=$originThere
echo timcapsThere=$timcapsThere

read -p "GO? Yes:CR No:nCR : " reply
echo svar: $reply


if [[ $reply = "" ]]
then
    echo OK... ;

    for rmt in $remotes;
    do
		echo Removing  "$rmt" ... ;
        git.exe remote remove $rmt ;
	done
	
	originSet="false"
	if [[ $githubThere = "true" ]]
	then
		git.exe remote add origin git@github.com:ekrogh/$dir2.git
		originSet="true"
		git.exe remote set-url --add --push origin git@github.com:ekrogh/$dir2.git ;
	fi
	if [[ $misoftThere = "true" ]]
	then
		if [[ $originSet = "false" ]]
		then
			git.exe remote add origin git@ssh.dev.azure.com:v3/ekrogh/$dir2/$dir2 ;
			originSet="true"
		fi
	fi
	if [[ $buffaloThere = "true" ]] || [[ $timcapsThere = "true" ]]
	then
		if [[ $originSet = "false" ]]
		then
			git.exe remote add origin //BUFFALO/share/GIT_Root/$dir1$dir2 ;
		fi
	fi
	git.exe remote set-url --add --push origin git@ssh.dev.azure.com:v3/ekrogh/$dir2/$dir2 ;
	git.exe remote set-url --add --push origin //BUFFALO/share/git_Root/$dir1$dir2 ;


    remotes=$(git.exe remote show)
    for rmt in $remotes;
    do
        git.exe remote show $rmt
    done
    
else
    echo Quit
fi
