import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {LineupType} from "../../types/lineup";
import {LineupsAPI} from "../../http/api/LineupsAPI";
import {AxiosError} from "axios";
import NotFound from "../../pages/NotFound";


type LineupParams = {
    id: string
}

const Lineup: React.FC = () => {
    const params = useParams<LineupParams>();
    const [lineup,setLineup] = useState<LineupType>()
    useEffect(()=>{
        LineupsAPI.getVerifiedLineupById(Number(params.id)).then(data => {
            setLineup(data)
        }).catch((error: AxiosError)=>{

        });
    },[])
    if(!lineup){
        return <NotFound/>
    }


    return (
        <div style={{width: '100%', height: '100%'}}>

        </div>
    );
};

export default Lineup;