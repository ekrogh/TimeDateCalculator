echo "rm -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/TimeDateCalculator/TimeDateCalculator.macOS/bin/Release/TimeDateCalculator.app"
rm -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/TimeDateCalculator/TimeDateCalculator.macOS/bin/Release/TimeDateCalculator.app
echo "cp -rf ..."
cp -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/TimeDateCalculator_.app /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/TimeDateCalculator.app

# echo "/usr/bin/lipo"
# /usr/bin/lipo /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app/Contents/MonoBundle/libMonoPosixHelper.dylib -remove i386 -output /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app/Contents/MonoBundle/libMonoPosixHelper.dylib

echo "Target _CodesignNativeLibraries:"
    /usr/bin/codesign -v --force --sign D2D049AFE3F1CE5EE2B477F41BE4B8915AED2504 /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app/Contents/MonoBundle/libMonoPosixHelper.dylib
echo  "Target _CodesignAppBundle:"
    /usr/bin/codesign -v --force --sign D2D049AFE3F1CE5EE2B477F41BE4B8915AED2504 --entitlements /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/obj/x64/Release/Entitlements.xcent /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app
echo "Target _CodesignVerify:"
    /usr/bin/codesign --verify -vvvv --deep /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app
#    echo "bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app: valid on disk"
#    echo "bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app: satisfies its Designated Requirement"
echo "rm -f ...pkg"
    rm -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/macOS_Pkgs/timeDateCalculator.pkg
echo "Target _CreateInstaller:"
#    Creating installer package
    /usr/bin/productbuild --component /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.app /Applications/ --sign "3rd Party Mac Developer Installer: Eigil Krogh (4657Q2Y6NH)" /Users/eks/projects/xamarinProjs/TimeDateCalculator/macOS_Pkgs/timeDateCalculator.pkg 
#  echo "claening up..."
#     rm -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/obj/
#     rm -rf /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/
    