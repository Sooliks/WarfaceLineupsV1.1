import React, {useEffect, useState} from 'react';
import {Button, Card, Select, Space, Tooltip} from "antd";
import {MapsAPI} from "../../http/api/MapsAPI";
import Search from "antd/es/input/Search";
import {CloseOutlined} from "@ant-design/icons";


type FilterProps = {
    onChange: (filter: FilterType) => void
    direction: 'horizontal' | 'vertical'
    searchVisible: boolean
    widthItemFilter?: number
}
export type FilterType = {
    typeMap: number
    typeSide: number
    typeFeature: number
    typePlant: number
    search: string
}

const Filter: React.FC<FilterProps> = ({onChange, direction, searchVisible, widthItemFilter = 260}) => {
    const defaultFilter: FilterType = {
        typeMap: 10,
        typeSide: 10,
        typeFeature: 10,
        typePlant: 10,
        search: ""
    }
    const [filter,setFilter] = useState<FilterType>(defaultFilter)
    const [maps,setMaps] = useState<{value: number, label: string}[]>([{value: 10, label: 'Выберите карту'}]);
    useEffect(()=>{
        onChange(filter);
        console.log(filter)
    },[filter])
    useEffect(()=>{
        MapsAPI.getMaps().then(_maps=> {
            _maps.map(m=>setMaps([...maps, {value: m.id, label: m.name}]))
        }).catch((e)=>{});
    },[])
    const dropFilter = () => {
        setFilter(defaultFilter);
    }

    return (
        <div style={{width: direction === "horizontal" ? "100%" : 300}}>
            <Card title={"Фильтр"} >
                <Space direction={direction} style={{width: '100%', justifyContent: 'space-between'}}>
                    <Select
                        value={filter.typeMap}
                        style={{width: direction === "horizontal" ? widthItemFilter : "100%"}}
                        placeholder="Выберите карту"
                        size={"large"}
                        className={"filterMap"}
                        onChange={(value)=> setFilter({...filter, typeMap: value})}
                        options={maps}
                        filterOption={(input, option) =>
                            (option?.label ?? '').toLowerCase().includes(input.toLowerCase())
                        }
                        showSearch
                    />
                    <Select
                        value={filter.typeFeature}
                        style={{width: direction === "horizontal" ? widthItemFilter : "100%"}}
                        placeholder="Выберите тип гранаты"
                        size={"large"}
                        className={"filterFeature"}
                        onChange={(value)=> setFilter({...filter, typeFeature: value})}
                        options={[
                            { value: 10, label: 'Выберите тип гранаты' },
                            { value: 1, label: 'Дымовая граната' },
                            { value: 2, label: 'Осколочная граната' },
                            { value: 3, label: 'Коктейль молотова' },
                            { value: 4, label: 'Светошумовая граната' },
                        ]}
                    />
                    <Select
                        value={filter.typeSide}
                        style={{width: direction === "horizontal" ? widthItemFilter : "100%"}}
                        placeholder="Выберите тип стороны"
                        size={"large"}
                        className={"filterSide"}
                        onChange={(value)=> setFilter({...filter, typeSide: value})}
                        options={[
                            { value: 10, label: 'Выберите тип стороны' },
                            { value: 0, label: 'Атака' },
                            { value: 1, label: 'Защита' },
                        ]}
                    />
                    <Select
                        value={filter.typePlant}
                        placeholder="Выберите плент"
                        style={{width: direction === "horizontal" ? widthItemFilter : "100%"}}
                        size={"large"}
                        className={"typePlant"}
                        onChange={(value)=> setFilter({...filter, typePlant: value})}
                        options={[
                            { value: 10, label: 'Выберите плент' },
                            { value: 1, label: '1' },
                            { value: 2, label: '2' },
                        ]}
                    />
                    {searchVisible && direction !== "vertical" &&
                        <Space direction="vertical">
                            <Search size={"large"} placeholder="Поиск по названию" allowClear onSearch={(value)=> setFilter({...filter, search: value})}
                                    style={{width: direction === "horizontal" ? widthItemFilter : "100%"}}/>
                        </Space>
                    }
                    {direction === "horizontal" ?
                        <Tooltip placement="top" title={"Сбросить фильтр"}>
                            <Button icon={<CloseOutlined />} style={{width: 40, height: 40}} onClick={dropFilter}/>
                        </Tooltip>
                        :
                        <Button style={{width: '100%'}} onClick={dropFilter}>Сбросить фильтр</Button>
                    }
                </Space>
            </Card>
            {searchVisible && direction === "vertical" &&
                <Card title="Найти" style={{marginTop: 20}}>
                    <Search size={"large"} placeholder="Поиск по названию" allowClear onSearch={(value)=> setFilter({...filter, search: value})}
                            style={{width: "100%"}}/>
                </Card>
            }
        </div>
    );
};

export default Filter;