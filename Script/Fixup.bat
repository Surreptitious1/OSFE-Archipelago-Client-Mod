
cd C:\Program Files (x86)\Steam\steamapps\common\One Step From Eden\OSFE_Data\StreamingAssets\Mods\osfe_archi

del Archipelago.MultiClient.Net.pdb
del Archipelago.MultiClient.Net.xml
del OSFE_Archipelago_Client.dll.config

mkdir deps\
move Archipelago.MultiClient.Net.dll deps\Archipelago.Multiclient.Net.dll
move Newtonsoft.Json.dll deps\Newtonsoft.Json.dll
