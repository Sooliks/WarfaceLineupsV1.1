import React from 'react';
import Filter from "../components/ui/Filter";
import {Affix} from "antd";

const Lineups: React.FC = () => {
    return (
        <div style={{width: '100%', height: '100%'}}>
            <Affix offsetTop={64}>
                <Filter onChange={()=>{}} direction={"horizontal"} searchVisible={true}/>
            </Affix>
        </div>
    );
};

export default Lineups;