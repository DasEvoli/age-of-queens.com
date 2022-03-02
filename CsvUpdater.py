import asyncio
import requests
import os

dir_path = os.path.dirname(os.path.realpath(__file__))
SAVE_PATH = '/Csv/'

async def download_csv(url :str, filename :str):
    try:
        r = requests.get(url, allow_redirects=True)
        open(dir_path + SAVE_PATH + filename, 'wb').write(r.content)
        r.close()
    except Exception as e:
        print(e)

async def loop():
    while True:
        await asyncio.sleep(600)

        url_casting_roster = 'https://docs.google.com/spreadsheets/d/1zmYGhrCTQN3L5WDFfAYkThcuJRKOMLKE2MOvBykbT7I/export?format=csv&id=1zmYGhrCTQN3L5WDFfAYkThcuJRKOMLKE2MOvBykbT7I&gid=0'
        url_solo_seed = 'https://docs.google.com/spreadsheets/d/1ALlkjl-jcCAHdapkVypjhUGkfyaNf3ebIUUw9F6APE8/export?format=csv&id=1ALlkjl-jcCAHdapkVypjhUGkfyaNf3ebIUUw9F6APE8&gid=0'
        url_leaderboard = 'https://docs.google.com/spreadsheets/d/1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E/export?format=csv&id=1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E&gid=121628420'
        url_blogposts = 'https://docs.google.com/spreadsheets/u/1/d/1CN9ztxk-2IHs5SfhSGHKWRU0oULHVVe3GdkOjAwwkIM/export?format=csv&id=1CN9ztxk-2IHs5SfhSGHKWRU0oULHVVe3GdkOjAwwkIM&gid=1046398781'
        url_teamseed = 'https://docs.google.com/spreadsheets/d/1Ljr1248kdWjpxuOuSE_Y-mDNLUGn00zoBehrStFhsnQ/export?format=csv&id=1Ljr1248kdWjpxuOuSE_Y-mDNLUGn00zoBehrStFhsnQ&gid=0'
        #url_introductions = 'https://docs.google.com/spreadsheets/d/1I-3c8zzonzmsOIJGejQDVEcmaTKj3shBEQvKyFCaKgs/export?format=csv&id=1I-3c8zzonzmsOIJGejQDVEcmaTKj3shBEQvKyFCaKgs&gid=1821488200'
        url_mods = 'https://docs.google.com/spreadsheets/d/1lxuqnQGlkopeHeKBp8qDMGgFC72Cy5ZGWBA5kks-VBc/export?format=csv&id=1lxuqnQGlkopeHeKBp8qDMGgFC72Cy5ZGWBA5kks-VBc&gid=0'
        url_activeevent = 'https://docs.google.com/spreadsheets/d/1ro9r0h6xNabPvelKh7pviK682tTQE_bLCBrvXkta_Hc/export?format=csv&id=1ro9r0h6xNabPvelKh7pviK682tTQE_bLCBrvXkta_Hc&gid=1420222812'
        await download_csv(url_casting_roster, 'CastingRoster.csv')
        await download_csv(url_solo_seed, 'SoloSeed.csv')
        await download_csv(url_leaderboard, 'LeaderboardRM.csv')
        await download_csv(url_blogposts, 'BlogPosts.csv')
        await download_csv(url_teamseed, 'TeamSeed.csv')
        await download_csv(url_mods, 'Mods.csv')
        await download_csv(url_activeevent, 'Active_Event_Data.csv')
        #await download_csv(url_introductions, 'Introductions.csv')

            


asyncio.run(loop())