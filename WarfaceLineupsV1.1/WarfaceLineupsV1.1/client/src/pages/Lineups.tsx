import React from 'react';
import Filter from "../components/ui/Filter";

const Lineups: React.FC = () => {
    return (
        <div style={{width: '100%', height: '100%'}}>
            <Filter onChange={()=>{}} direction={"horizontal"} searchVisible={true}/>
        </div>
    );
};

export default Lineups;